function getUserIP() {
    fetch('https://api.ipify.org?format=json')
        .then(response => response.json())
        .then(data => {
            let userIP = data.ip;
            loginUser(userIP);
        })
        .catch(error => console.error('Error fetching IP:', error));
}
function loginUser(userIP) {

    if (validateCaptcha()) {
        const phone = document.getElementById("login-phone").value;
        const password = document.getElementById("login-password").value;


        fetch('/Auth/Login', {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify({ PhoneNumber: phone, Password: password, IPAddress: userIP })
        })
            .then(response => response.json())
            .then(result => {
                if (result.resultCode === 201) {
                    MessageShow("عملیات با موفقیت انجام شد", result.resultMessage, "موفق", 3000);
                    localStorage.setItem("AuthToken", result.token);
                    window.location.href = dashboardUrl;
                    
                } else {
                    MessageShow("خطا", result.resultMessage, "error", 3000);
                }
            })
            .catch(error => console.error('Error:', error));
        MessageShow("خطای سرور",result.resultMessage, "error", 3000);
    } else {
        MessageShow("کپچا نامعتبر", "لطفاً دوباره تلاش کنید.", "warning", 3000);
    }
}


function showForgotPassword() {
    document.getElementById("login-form").style.display = "none";
    document.getElementById("forgot-password-form").style.display = "block";
}


function showLogin() {
    document.getElementById("forgot-password-form").style.display = "none";
    document.getElementById("login-form").style.display = "block";
}


function sendVerificationCode() {
    const phoneNumber = document.getElementById("forgot-phone").value;

    if (!phoneNumber) {
        alert("Please enter your phone number.");
        return;
    }

    fetch('/Auth/GetVerificationCode', {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json'
        },
        body: JSON.stringify({ PhoneNumber: phoneNumber })
    })
        .then(response => response.json())
        .then(result => {
            if (result.resultCode === 202) {
                MessageShow("عملیات با موفقیت انجام شد ", result.resultMessage, "موفق",3000);

                document.getElementById("forgot-password-form").style.display = "none";
                document.getElementById("enter-verification-code-form").style.display = "block";
            } else {
                alert(result.resultMessage);
            }
        })
        .catch(error => console.error('Error:', error));
}


function changePassword() {
    const verificationCode = document.getElementById("verification-code").value;
    const newPassword = document.getElementById("new-password").value;
    const confirmPassword = document.getElementById("confirm-password").value;
    const phoneNumber = document.getElementById("forgot-phone").value;

    if (newPassword !== confirmPassword) {
        alert("Passwords do not match.");
        return;
    }

    fetch('/Auth/ChangePassword', {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json'
        },
        body: JSON.stringify({
            PhoneNumber: phoneNumber,
            VerificationCode: verificationCode,
            NewPassword: newPassword,
            ConfirmPassword: confirmPassword
        })
    })
        .then(response => response.json())
        .then(result => {
            if (result.resultCode === 204) {
                MessageShow("عملیات با موفقیت انجام شد ", result.resultMessage, "موفق", 3000);
                document.getElementById("login-form").style.display = "block";
                document.getElementById("enter-verification-code-form").style.display = "none";
            } else {
                alert(result.resultMessage);
            }
        })
        .catch(error => console.error('Error:', error));
}
//------------Captcha-----------//
const characters = 'ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789';
function generateCaptcha() {
    let captcha = '';
    for (let i = 0; i < 6; i++) {
        captcha += characters.charAt(Math.floor(Math.random() * characters.length));
    }
    document.getElementById('captcha').textContent = captcha;
    document.getElementById('captcha-input').value = '';
    document.getElementById('captcha-error').style.display = 'none';
}

function validateCaptcha() {
    const captchaText = document.getElementById('captcha').textContent;
    const userInput = document.getElementById('captcha-input').value;

    if (captchaText === userInput) {
        return true;
    } else {
        document.getElementById('captcha-error').style.display = 'block';
        return false;
    }
}

window.onload = generateCaptcha;
//-----------------------//
function logoutUser() {
    fetch('/Auth/LogOut', {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json'
        }
    })
        .then(response => response.json())
        .then(result => {
            if (result.resultCode === 205) {
                MessageShow("عملیات با موفقیت انجام شد ", result.resultMessage, "موفق", 3000);
                window.location.href = "/Home/Index";
            } else {
                MessageShow("خطای سرور", result.resultMessage, "error", 3000);
            }
        })
        .catch(error => {
            console.error('Error:', error);
        });
}

