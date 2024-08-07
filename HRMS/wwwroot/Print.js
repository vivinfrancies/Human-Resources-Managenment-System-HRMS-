window.downloadPDF = async () => {
    console.log('downloadPDF function called');
    const { jsPDF } = window.jspdf;
    const doc = new jsPDF();

    const element = document.querySelector('.payslip-container');

    await html2canvas(element, { scale: 2 }).then((canvas) => {
        const imgData = canvas.toDataURL('image/png');
        const imgProps = doc.getImageProperties(imgData);
        const pdfWidth = doc.internal.pageSize.getWidth();
        const pdfHeight = (imgProps.height * pdfWidth) / imgProps.width;

        doc.addImage(imgData, 'PNG', 0, 0, pdfWidth, pdfHeight);
        doc.save('Payslip.pdf');
    });
};



