
function submitForm() {

    var formData = {
        FirstName: document.getElementById('firstName').value,
        LastName: document.getElementById('lastName').value,
        Email: document.getElementById('email').value,
        NationalNumber: document.getElementById('nationalNumber').value,
        PhoneNumber: document.getElementById('phoneNumber').value,
        Title: document.getElementById('title').value,
        Province: document.getElementById('province').value,
        City: document.getElementById('city').value,
        Address: document.getElementById('address').value,
        Description: document.getElementById('description').value
    };


    fetch('/Seller/UpdateAndInsertProfile', {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(formData)
    })
        .then(response => response.json())
        .then(data => {
            if (data.resultCode === 200) {
                alert(data.resultMessage);
            } else {
                alert("خطایی در به‌روزرسانی اطلاعات رخ داده است.");
            }
        })
        .catch(error => {
            console.error('Error:', error);
        });
}