
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
//-------------ChangePassword&LogOut-------------//
function showChangePasswordModal() {
    document.getElementById("changePasswordModal").style.display = "block";
}
function closeModal() {
    document.getElementById("changePasswordModal").style.display = "none";
}
function sendVerificationCode() {
    const phoneNumber = document.getElementById("forgot-phone").value;


    fetch('/Auth/GetVerificationCode', {
        method: 'POST',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify({ PhoneNumber: phoneNumber })
    })
        .then(response => response.json())
        .then(result => {
            if (result.resultCode === 200) {
                alert("کد تایید ارسال شد.");
                document.getElementById("changePasswordStep1").style.display = "none";
                document.getElementById("changePasswordStep3").style.display = "block";
            } else {
                alert(result.resultMessage);
            }
        })
        .catch(error => console.error('Error:', error));
}
function changePassword() {
    const phoneNumber = document.getElementById("forgot-phone").value;
    const newPassword = document.getElementById("new-password").value;
    const confirmPassword = document.getElementById("confirm-password").value;
    const verificationCode = document.getElementById("verification-code").value;  

    if (newPassword !== confirmPassword) {
        alert("رمز عبور جدید و تایید آن یکسان نیستند.");
        return;
    }

    fetch('/Auth/ChangePassword', {
        method: 'POST',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify({
            PhoneNumber: phoneNumber,
            NewPassword: newPassword,
            VerificationCode: verificationCode 
        })
    })
        .then(response => response.json())
        .then(result => {
            if (result.resultCode === 200) {
                alert("رمز عبور با موفقیت تغییر یافت.");
                closeModal();  
            } else {
                alert(result.resultMessage);
            }
        })
        .catch(error => console.error('Error:', error));
}
function logoutUser() {
    fetch('/Auth/LogOut', {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json'
        }
    })
        .then(response => response.json())
        .then(data => {
            if (data.resultCode === 200) {
                alert(data.resultMessage);
                window.location.href = "/Home/Index";
            } else {
                alert("خطایی در خروج رخ داده است.");
            }
        })
        .catch(error => {
            console.error('Error:', error);
        });
}
//-------------ChangePassword&LogOut-------------//