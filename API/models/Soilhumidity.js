const mongoose = require('mongoose');

const SoilHumiditySchema = mongoose.Schema({
    humidity: {
        type: String,
        required: true
    },
    date: {
        type: Date,
        required: true,
        default: Date.now
    },
    plantId: {
        type: String,
        required: true
    }
});

module.exports = mongoose.model('Soilhumidity', SoilHumiditySchema, 'soilhumidities');