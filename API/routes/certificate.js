const express = require('express');
const Temperature = require('../models/Temperature');
const Airhumidity = require('../models/Airhumidity');
const Soilhumidity = require('../models/Soilhumidity');
const fs = require('fs');
const { createCertificate } = require('../pdf/certificate');
const { ServerlessApplicationRepository } = require('aws-sdk');
const { cwd } = require('process');

const router = express.Router();

//Generate certificate
router.get('/:plantId', async (req, res) => {
    let temperatures;
    let soilhumidities;
    let airhumidities;
    try {
        temperatures = await Temperature.find({ plantId: req.params.plantId }).sort().limit(10);
    } catch (err) {
        console.log(err);
    }
    try {
        soilhumidities = await Soilhumidity.find({ plantId: req.params.plantId }).sort().limit(10);
    } catch (err) {
        console.log(err);
    }
    try {
        airhumidities = await Airhumidity.find({ plantId: req.params.plantId }).sort().limit(10);
    } catch (err) {
        console.log(err);
    }

    try {
        console.log('file downloaded1');
        createCertificate(temperatures, soilhumidities, airhumidities);
        console.log('file downloaded2');
        setTimeout(() => {
            res.download('certificate.pdf');
        }, 1000);
    } catch (err) {
        console.log(err);
    }
})

module.exports = router;