const bodyParser = require('body-parser');
const express = require('express');
const mongoose = require('mongoose');
const app = express();
require('dotenv/config');

const port = process.env.PORT || 3000

/*const outfitDataRoutes = require('./routes/outfitData');
const imageRoutes = require('./routes/image');
const pieceRoutes = require('./routes/piece');*/
const plantRoutes = require('./routes/plant')

app.use(bodyParser.urlencoded({ extended: false }));
app.use(bodyParser.json());

/*app.use('/outfit', outfitDataRoutes);
app.use('/image', imageRoutes);
app.use('/piece', pieceRoutes);*/
app.use('/plant', plantRoutes);

app.get('/', (req, res) => {
    res.send('Outfitapp API');
})

mongoose.connect(process.env.DB_CONNECTION, { useNewUrlParser: true, useUnifiedTopology: true }, () => {
    console.log('connected');
});

app.listen(port);
