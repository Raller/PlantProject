const mongoose = require('mongoose');

const ErrorSchema = mongoose.Schema({
    type: {
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

module.exports = mongoose.model('Error', ErrorSchema, 'errors');