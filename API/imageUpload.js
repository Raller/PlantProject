const multer = require('multer');
const aws = require('aws-sdk');
const multerS3 = require('multer-s3');
require('dotenv/config');

aws.config.update({
    secretAccessKey: process.env.SECRET_ACCESS_KEY,
    accessKeyId: process.env.ACCESS_KEY_ID,
    region: 'eu-central-1'
})

const s3 = new aws.S3();

const upload = multer({
    storage: multerS3({
        s3: s3,
        bucket: 'plantprojectimages',
        acl: 'public-read',
        key: (req, file, cb) => {
            cb(null, Date.now().toString() + file.originalname)
        }
    })
})


//https://stackoverflow.com/questions/55963408/which-is-the-best-way-to-store-images-for-expressjs-mongodb-site
module.exports = upload;