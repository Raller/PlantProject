const express = require('express');
const Plant = require('../models/Plant');
const upload = require('../imageUpload');

const router = express.Router();

//Get all plants
router.get('/', async (req, res) => {
    try {
        const plants = await Plant.find();
        return res.status(200).json(plants);
    } catch (err) {
        return res.json(err);
    }
})

//Post plant
router.post('/', upload.single('image'), async (req, res) => {

    console.log(req.file.location);

    const plant = new Plant({
        name: req.body.name,
        imageUrl: req.file.location,
        type: req.body.type,
        longitude: req.body.longitude,
        latitude: req.body.latitude
    });

    try {
        const savedPlant = await plant.save();
        return res.status(200).json(savedPlant);
    } catch (err) {
        console.log(err);
        return res.json({ message: err });
    }
})

//Get specific plant by ID
router.get('/id/:plantId', async (req, res) => {
    try {
        const plant = await Plant.findById(req.params.plantId);
        return res.status(200).json(plant);
    } catch (err) {
        return res.json(err);
    }
})

//Find plant by name
router.get('/name/:name', async (req, res) => {
    try {
        const plant = await Plant.findOne({ name: req.params.name });
        return res.status(200).json(plant);
    } catch (err) {
        return res.json(err);
    }
})

//Delete plant by id
router.delete('/id/:plantId', async (req, res) => {
    try {
        const removedPlant = Plant.deleteOne({ _id: req.params.plantId });
        return res.status(200).json(removedPlant);
    } catch (err) {
        res.json(err);
    }
})

//Delete plant by name
router.delete('/name/:name', async (req, res) => {
    try {
        const removedPlant = Plant.deleteOne({ name: req.params.name });
        return res.status(200).json(removedPlant);
    } catch (err) {
        res.json(err);
    }
})

//Update plant by id
router.put('/id/:plantId', async (req, res) => {
    try {
        const updatedPlant = await Plant.findByIdAndUpdate({ _id: req.params.plantId }, req.body);
        res.json(updatedPlant);
    } catch (err) {
        res.json(err);
    }
})

//Update plant by name
router.put('/name/:name', async (req, res) => {
    try {
        const updatedPlant = await Plant.findByIdAndUpdate({ name: req.params.name }, req.body);
        res.json(updatedPlant);
    } catch (err) {
        res.json(err);
    }
})

module.exports = router;