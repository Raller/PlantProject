const PDFDocument = require('pdfkit');
const fs = require('fs');

//Generate certificate pdf
const createCertificate = (temperatures, soilhumidities, airhumidities) => {
    const doc = new PDFDocument();
    doc.pipe(fs.createWriteStream('certificate.pdf'));
    doc.fontSize(42).text('CERTIFIKAT', { align: 'center' });
    doc.image('./pdf/tree.png', {
        width: 225,
        height: 225,
        x: (doc.page.width - 225) / 2
    })

    let col1LeftPos = 25;
    let colTop = 400;
    let colTextTop = 425;
    let colWidth = 125;
    let col2LeftPos = colWidth + col1LeftPos + 20;
    let col3LeftPos = colWidth + col2LeftPos + 20;
    let col4LeftPos = colWidth + col3LeftPos + 20;


    doc.fontSize(16)
        .text('Dato', col1LeftPos, colTop, { width: colWidth, align: 'center' })
        .text('Temperatur', col2LeftPos, colTop, { width: colWidth, align: 'center' })
        .text('Luftfugtighed', col3LeftPos, colTop, { width: colWidth, align: 'center' })
        .text('Jordfugtighed', col4LeftPos, colTop, { width: colWidth, align: 'center' });

    temperatures.forEach(temp => {
        const date = new Date(temp.date);
        doc.fontSize(12).text(date.toLocaleString('da-DK'), col1LeftPos, colTextTop, { width: colWidth, align: 'center' });
        colTextTop += 15;
    });

    colTextTop = 425;
    temperatures.forEach(temp => {
        doc.fontSize(12).text(temp.temperature + ' grader', col2LeftPos, colTextTop, { width: colWidth, align: 'center' });
        colTextTop += 15;
    })

    colTextTop = 425;
    airhumidities.forEach(hum => {
        doc.fontSize(12).text(hum.humidity + '%', col3LeftPos, colTextTop, { width: colWidth, align: 'center' });
        colTextTop += 15;
    })

    colTextTop = 425;
    soilhumidities.forEach(hum => {
        if (hum.humidity === 'Dry') {
            doc.fontSize(12).fillColor('red').text(hum.humidity, col4LeftPos, colTextTop, { width: colWidth, align: 'center' });
        } else {
            doc.fontSize(12).fillColor('green').text(hum.humidity, col4LeftPos, colTextTop, { width: colWidth, align: 'center' });

        }
        colTextTop += 15;
    })

    doc.end();
}

exports.createCertificate = createCertificate;