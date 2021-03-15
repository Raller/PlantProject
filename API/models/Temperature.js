const mongoose = require('mongoose');

const TemperatureSchema = mongoose.Schema({
    temperature: {
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

module.exports = mongoose.model('Temperature', TemperatureSchema, 'temperatures');