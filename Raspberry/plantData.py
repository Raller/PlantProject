import RPi.GPIO as GPIO
from datetime import datetime
import board
import busio
import adafruit_am2320
import time
import requests

RED_LED = 12
GREEN_LED = 25
SOIL = 27

GPIO.setup(RED_LED,GPIO.OUT)
GPIO.setup(GREEN_LED,GPIO.OUT)
GPIO.setup(SOIL,GPIO.IN)

i2c = busio.I2C(board.SCL, board.SDA)

#Method for config setup
#Asks the user for input about all the required info config
def configSetup():
  #dbServer = input("Input the server address for DB : ")
  dbServer = "https://plantprojectapi.azurewebsites.net/"
  plantName = input("What is the name of the plant : ")
  plantID = input("Whats the ID for the plant : ")
  interval = input("How often should it record, in minutes : ")
  mintemp = input("What is the minimum temperature for this plant, in celsius : ")

  #Creates a new config or deletes all within the config
  f = open("plantConfig.txt","w")
  f.close()

  #Opens config to write new desired configurations
  cf = open("plantConfig.txt","a")
  cf.write("Server =" + dbServer + " \n")
  cf.write("PlantName =" + plantName + " \n")
  cf.write("PlantID =" + plantID + " \n")
  cf.write("Interval =" + interval + " \n")
  cf.write("MinTemp =" + mintemp + " \n")

  cf.close()

#A small method to display the configurations to the user
def readConfig():
  f = open("plantConfig.txt","r")
  print("Reading" + f.read())
  f.close()

  input("Enter to continue")

#This method is used to read out a specific config file
#this helps to get the proper configuration depending on what is needed
def readConfigLine(cLine):
  cf = open("plantConfig.txt","r")

  lineNum = 0
  readIt = False
  doneR = False
  returnLine = ""

  for line in cf:
    if(lineNum == cLine):
      for character in line:
        if(character == "=" or readIt == True):
          if(character.isspace() or doneR == True):
           doneR = True
          else:
           readIt = True
           if(character != "="):
             returnLine = returnLine + character

    lineNum = lineNum + 1
  #print(returnLine)
  return returnLine

#These following 3 methods are all used to post to the database
#For air temperature, air humidity and the soil humidity
def tempPost(dataTemp):
  dbServerA = readConfigLine(0)
  plantID = readConfigLine(2)
  
  dbServerA = dbServerA + "temperature/"
  
  response = requests.post(dbServerA, data = {'temperature':dataTemp, 'plantId':plantID})
  
def humidPost(dataHumid):
  dbServerA = readConfigLine(0)
  plantID = readConfigLine(2)
  
  dbServerA = dbServerA + "airhumidity/"
  
  response = requests.post(dbServerA, data = {'humidity':dataHumid, 'plantId':plantID})
  
def soilHumidPost(dataSoil):
  dbServerA = readConfigLine(0)
  plantID = readConfigLine(2)
  
  dbServerA = dbServerA + "soilhumidity/"
  
  response = requests.post(dbServerA, data = {'humidity':dataSoil, 'plantId':plantID})
  
#Here starts the logging method
#Used to measure and log the air temperature and humidity
#As well as the soil humidity, if it is dry or not
def startLog():
  drySoil = False
  
  dbServer = readConfigLine(0)
  plantID = readConfigLine(2)
  intervalC = readConfigLine(3)
  minimumTemp = readConfigLine(4)

  floatMinTemp = float(minimumTemp)

  done = False
  
  #This while loop is the whole measuring and logging process
  while(True):
    interval = int(intervalC)
    interval = interval * 60
    while interval > 0:
      interval -= 1
      time.sleep(1)

    sensor = adafruit_am2320.AM2320(i2c)

    humidity = sensor.relative_humidity
    temperature = sensor.temperature

    #Checks with the soil input, if there the soil is dry or not
    if(GPIO.input(SOIL)):
      drySoil = True
      soilHumid = "Dry"
    else:
      drySoil = False
      soilHumid = "Not dry"
    
    #If the soil is dry or if the temperature in the air is low
    #Will enable an LED for red as a warning, or green as ok
    if(drySoil == True or temperature <= floatMinTemp):
      GPIO.output(RED_LED,True)
      GPIO.output(GREEN_LED,False)
    else:
      GPIO.output(RED_LED,False)
      GPIO.output(GREEN_LED,True)

    #Calling methods to post to the database
    tempPost(temperature)
    humidPost(humidity)
    soilHumidPost(soilHumid)

    time.sleep(1)
 
#The main loop and setup
#Asks the user for input as to what they want to do
try:
  GPIO.output(RED_LED,False)
  GPIO.output(GREEN_LED,False)
  reset = 1
  while(True):
    if(reset == 1):
      reset = 0
      print("-- What do you want to do ? --")
      print("1. Setup config for logging")
      print("2. Read the config")
      print("3. Enable the logging")
      print("------------------------------")

    choice = input("Use a number, 1, 2, or 3 : ")
    while(int(choice) > 3 or int(choice) < 1):
      choice = input("Please use only 1, 2 or 3 : ")

    if(int(choice) == 1 and reset == 0):
      reset = 1
      configSetup()
    if(int(choice) == 2 and reset == 0):
      reset = 1
      readConfig()
    if(int(choice) == 3 and reset == 0):
      reset = 1
      startLog()

except KeyboardInterrupt:
  print ("\n")

finally:
  GPIO.cleanup()
