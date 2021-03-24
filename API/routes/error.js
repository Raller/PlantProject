const express = require('express');
const { remove } = require('../models/Error');
const Error = require('../models/Error');

const router = express.Router();

//Get all error data
router.get('/', async (req, res) => {
    try {
        const errors = await Error.find();
        return res.status(200).json(errors);
    } catch (err) {
        return res.json(err);
    }
})

//Post new error data
router.post('/', async (req, res) => {

    const error = new Error({
        type: req.body.type,
        plantId: req.body.plantId
    });

    try {
        const savedError = await error.save();
        return res.status(200).json(savedError);
    } catch (err) {
        console.log(err);
        return res.json({ message: err });
    }
})

//Get specific error data by ID
router.get('/id/:errorId', async (req, res) => {
    try {
        const error = await Error.findById(req.params.errorId);
        return res.status(200).json(error);
    } catch (err) {
        return res.json(err);
    }
})

//Get error data by plantid
router.get('/plantid/:plantId', async (req, res) => {
    try {
        const errors = await Error.find({ plantId: req.params.plantId });
        return res.status(200).json(errors);
    } catch (err) {
        return res.json(err);
    }
})

//Delete error by id
router.delete('/id/:errorId', async (req, res) => {
    try {
        const removedError = await Error.deleteOne({ _id: req.params.errorId });
        return res.status(200).json(removedError);
    } catch (err) {
        res.json(err);
    }
})

//Delete errors by plantId
router.delete('/plantid/:plantId', async (req, res) => {
    try {
        const removedErrors = await Error.deleteMany({ plantId: req.params.plantId });
        return res.status(200).json(removedErrors);
    } catch (err) {
        res.json(err);
    }
})

module.exports = router;