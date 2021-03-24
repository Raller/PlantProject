const express = require('express');
const Soilhumidity = require('../models/Soilhumidity');

const router = express.Router();

//Get all soilhumidity data
router.get('/', async (req, res) => {
    try {
        const humidities = await Soilhumidity.find();
        return res.status(200).json(humidities);
    } catch (err) {
        return res.json(err);
    }
})

//Post new airhumidity data
router.post('/', async (req, res) => {

    const humidity = new Soilhumidity({
        humidity: req.body.humidity,
        plantId: req.body.plantId,
    });

    try {
        const savedHumidity = await humidity.save();
        return res.status(200).json(savedHumidity);
    } catch (err) {
        console.log(err);
        return res.json({ message: err });
    }
})

//Get specific humidity data by ID
router.get('/id/:humidityId', async (req, res) => {
    try {
        const humidity = await Soilhumidity.findById(req.params.humidityId);
        return res.status(200).json(humidity);
    } catch (err) {
        return res.json(err);
    }
})

//Find humidity data by plantid
router.get('/plantid/:plantId', async (req, res) => {
    try {
        const humidities = await Soilhumidity.find({ plantId: req.params.plantId });
        return res.status(200).json(humidities);
    } catch (err) {
        return res.json(err);
    }
})

//Get plant's latest humidity
router.get('/latest/:plantId', async (req, res) => {
    try {
        const humidity = await Soilhumidity.findOne({ plantId: req.params.plantId }).sort({ date: -1 }).limit(1);
        return res.status(200).json(humidity);
    } catch (err) {
        return res.json(err);
    }
})

//Get top 10 latest humidities
router.get('/latest/:plantId/:amount', async (req, res) => {
    try {
        const humidities = await Soilhumidity.find({ plantId: req.params.plantId }).sort({ date: -1 }).limit(parseInt(req.params.amount));
        return res.status(200).json(humidities);
    } catch (err) {
        return res.json(err);
    }
})

//Delete humidity by id
router.delete('/id/:humidityId', async (req, res) => {
    try {
        const removedHumidity = await Soilhumidity.deleteOne({ _id: req.params.humidityId });
        return res.status(200).json(removedHumidity);
    } catch (err) {
        res.json(err);
    }
})

//Delete humidity by plantId
router.delete('/plantid/:plantId', async (req, res) => {
    try {
        const removedSoilhumidity = await Soilhumidity.deleteMany({ plantId: req.params.plantId });
        return res.status(200).json(removedSoilhumidity);
    } catch (err) {
        res.json(err);
    }
})

module.exports = router;