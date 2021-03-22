const express = require('express');
const { remove } = require('../models/Temperature');
const Temperature = require('../models/Temperature');

const router = express.Router();

//Get all temperatures data
router.get('/', async (req, res) => {
    try {
        const temperatures = await Temperature.find();
        return res.status(200).json(temperatures);
    } catch (err) {
        return res.json(err);
    }
})

//Post new temperature data
router.post('/', async (req, res) => {

    const temperature = new Temperature({
        temperature: req.body.temperature,
        plantId: req.body.plantId,
    });

    try {
        const savedTemperature = await temperature.save();
        return res.status(200).json(savedTemperature);
    } catch (err) {
        console.log(err);
        return res.json({ message: err });
    }
})

//Get specific temperature data by ID
router.get('/id/:tempId', async (req, res) => {
    try {
        const temperature = await Temperature.findById(req.params.tempId);
        return res.status(200).json(temperature);
    } catch (err) {
        return res.json(err);
    }
})

//Find temperature data by plantid
router.get('/plantid/:plantId', async (req, res) => {
    try {
        const temperature = await Temperature.find({ plantId: req.params.plantId });
        return res.status(200).json(temperature);
    } catch (err) {
        return res.json(err);
    }
})

//Get plant's latest temperature
router.get('/latest/:plantId', async (req, res) => {
    try {
        const temperature = await Temperature.findOne({ plantId: req.params.plantId }).sort({ date: -1 }).limit(1);
        return res.status(200).json(temperature);
    } catch (err) {
        return res.json(err);
    }
})

//Get top 10 latest temperatures
router.get('/latest/:plantId/:amount', async (req, res) => {
    try {
        const temperature = await Temperature.find({ plantId: req.params.plantId }).sort({ date: -1 }).limit(parseInt(req.params.amount));
        return res.status(200).json(temperature);
    } catch (err) {
        return res.json(err);
    }
})

//Delete temperature by id
router.delete('/id/:tempId', async (req, res) => {
    try {
        const removedTemperature = await Temperature.deleteOne({ _id: req.params.tempId });
        return res.status(200).json(removedTemperature);
    } catch (err) {
        res.json(err);
    }
})

//Delete temperatures by plantId
router.delete('/plantid/:plantId', async (req, res) => {
    try {
        const removedTemperatures = await Temperature.deleteMany({ plantId: req.params.plantId });
        return res.status(200).json(removedTemperatures);
    } catch (err) {
        res.json(err);
    }
})


//Update plant by id
/*router.put('/id/:tempId', async (req, res) => {
    try {
        const updatedTemperature = await Temperature.findByIdAndUpdate({ _id: req.params.tempId }, req.body);
        res.json(updatedTemperature);
    } catch (err) {
        res.json(err);
    }
})*/

module.exports = router;