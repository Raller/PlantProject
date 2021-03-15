const mongoose = require('mongoose');

const PlantSchema = mongoose.Schema({
    name: {
        type: String,
        required: true
    },
    imageUrl: {
        type: String,
        required: true
    },
    type: {
        type: String,
        required: true
    },
    date: {
        type: Date,
        required: true,
        default: Date.now
    },
    longitude: {
        type: String,
        required: true
    },
    latitude: {
        type: String,
        required: true
    }

});

module.exports = mongoose.model('Plant', PlantSchema, 'plants');