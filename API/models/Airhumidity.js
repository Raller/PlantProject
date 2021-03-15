const mongoose = require('mongoose');

const AirhumiditySchema = mongoose.Schema({
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

module.exports = mongoose.model('Airhumidity', AirhumiditySchema, 'airhumidities');