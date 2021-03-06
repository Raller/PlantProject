const bodyParser = require('body-parser');
const express = require('express');
const mongoose = require('mongoose');
const Temperature = require('./models/Temperature');
const path = require('path');
const app = express();
require('dotenv/config');

const port = process.env.PORT || 3000

const airhumidityRoutes = require('./routes/airhumidity');
const soilhumidityRoutes = require('./routes/soilhumidity');
const temperatureRoutes = require('./routes/temperature');
const plantRoutes = require('./routes/plant')
const errorRoutes = require('./routes/error');
const certificateRoutes = require('./routes/certificate');



app.use(bodyParser.urlencoded({ extended: false }));
app.use(bodyParser.json());

app.use('/airhumidity', airhumidityRoutes);
app.use('/soilhumidity', soilhumidityRoutes);
app.use('/temperature', temperatureRoutes);
app.use('/plant', plantRoutes);
app.use('/error', errorRoutes);
app.use('/certificate', certificateRoutes);



app.get('/', (req, res) => {
    res.sendFile(path.join(__dirname, './index.html'));
})

mongoose.connect(process.env.DB_CONNECTION, { useNewUrlParser: true, useUnifiedTopology: true }, () => {
    console.log('connected');
});

app.listen(port);
