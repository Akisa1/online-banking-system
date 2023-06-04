

async function test() {

    document.getElementById("Name").value = "yunus"
    document.getElementById("Surname").value = "ileri"
    document.getElementById("Username").value = "yunus.ileri"
    document.getElementById("Identity").value = "82811543372"
    document.getElementById("E-Mail").value = "yunus@gmail.com"
    document.getElementById("phone").value = "055506649932"
    document.getElementById("Password").value = "12QWaszx*"
    document.getElementById("R-Password").value = "12QWaszx*"
}


async function validateForm() {
    let result = true;

    const name = document.getElementById("Name").value;
    const surname = document.getElementById("Surname").value;
    const username = document.getElementById("Username").value;
    const identity = document.getElementById("Identity").value
    const email = document.getElementById("E-Mail").value;
    const phone = document.getElementById("phone").value;
    const password = document.getElementById("Password").value;
    const confirmPassword = document.getElementById("R-Password").value;
    const birthDate = document.getElementById("Birth").value;



    if (!identity_dogrula(identity)) {
        document.getElementById("Identity").classList.remove("is-valid");
        document.getElementById("Identity").classList.add("is-invalid");
        result = false;
    } else {
        document.getElementById("Identity").classList.add("is-valid");
        document.getElementById("Identity").classList.remove("is-invalid");

    }

    // Name validation
    var namePattern = /^[A-Za-z]+$/;
    if (!name.match(namePattern)) {
        document.getElementById("Name").classList.add("is-invalid");
        document.getElementById("Name").classList.remove("is-valid");
        result = false;
    } else {
        document.getElementById("Name").classList.add("is-valid");
        document.getElementById("Name").classList.remove("is-invalid");
    }

    // Surname validation
    var surnamePattern = /^[A-Za-z]+$/;
    if (!surname.match(surnamePattern)) {
        document.getElementById("Surname").classList.add("is-invalid");
        document.getElementById("Surname").classList.remove("is-valid");
        result = false;
    } else {
        document.getElementById("Surname").classList.add("is-valid");
        document.getElementById("Surname").classList.remove("is-invalid");
    }

    // Username validation
    var usernamePattern = /^[A-Za-z0-9]+$/;
    if (!username.match(usernamePattern)) {
        document.getElementById("Username").classList.add("is-invalid");
        document.getElementById("Username").classList.remove("is-valid");
        result = false;
    } else {
        document.getElementById("Username").classList.add("is-valid");
        document.getElementById("Username").classList.remove("is-invalid");
    }
     

    // E-Mail validation
    var emailPattern = /^[^\s@]+@[^\s@]+\.[^\s@]+$/;
    if (!email.match(emailPattern)) {
        document.getElementById("E-Mail").classList.add("is-invalid");
        document.getElementById("E-Mail").classList.remove("is-valid");
        result = false;
    } else {
        document.getElementById("E-Mail").classList.add("is-valid");
        document.getElementById("E-Mail").classList.remove("is-invalid");
    }

    

     
    // Birth Date validation
    var today = new Date();
    var birthDateObj = new Date(birthDate);
    var age = today.getFullYear() - birthDateObj.getFullYear();
    var monthDiff = today.getMonth() - birthDateObj.getMonth();
    if (
        monthDiff < 0 ||
        (monthDiff === 0 && today.getDate() < birthDateObj.getDate())
    ) {
        age--;
    }
    if (age < 18 || birthDateObj == "Invalid Date") {
        document.getElementById("Birth").classList.add("is-invalid");
        document.getElementById("Birth").classList.remove("is-valid");
        result = false;
    } else {
        document.getElementById("Birth").classList.add("is-valid");
        document.getElementById("Birth").classList.remove("is-invalid");
    }



    // Password validation
    if (password !== confirmPassword || !password || !confirmPassword) {
        document.getElementById("Password").classList.add("is-invalid");
        document.getElementById("Password").classList.remove("is-valid");
        document.getElementById("R-Password").classList.add("is-invalid");
        document.getElementById("R-Password").classList.remove("is-valid");
        result = false;
    } else {
        document.getElementById("Password").classList.add("is-valid");
        document.getElementById("Password").classList.remove("is-invalid");
        document.getElementById("R-Password").classList.add("is-valid");
        document.getElementById("R-Password").classList.remove("is-invalid");

    }

     

    if (result === true) {

        const user = {
            Name: name,
            Surname: surname,
            Email: email,
            PhoneNumber: phone,
            IdentityNumber: identity,
            Password: password,
            UserName: username,
            birthDate: birthDate
        }
        await registerUser(user)
    }
}



function identity_dogrula(identity) {
    identity = String(identity);

    if (identity.substring(0, 1) === "0") {
        return false;
    }
    if (identity.length !== 11) {
        return false;
    }
    var ilkon_array = identity.substr(0, 10).split("");
    var ilkon_total = (hane_tek = hane_cift = 0);

    for (var i = (j = 0); i < 9; ++i) {
        j = parseInt(ilkon_array[i], 10);
        if (i & 1) {
            // tek ise, tcnin çift haneleri toplanmalı!
            hane_cift += j;
        } else {
            hane_tek += j;
        }
        ilkon_total += j;
    }
    if (
        (hane_tek * 7 - hane_cift) % 10 !==
        parseInt(identity.substr(-2, 1), 10)
    ) {
        return false;
    }
    ilkon_total += parseInt(ilkon_array[9], 10);
    if (ilkon_total % 10 !== parseInt(identity.substr(-1), 10)) {
        return false;
    }

    return true;
}
 


async function registerUser(user) {

    try {
        const response = await $.ajax({
            url: "./PostRegister",
            type: "POST",
            data: JSON.stringify(user),
            contentType: "application/json"

        });
      
        if (response > 0) {
            alert("Kullanıcı oluşturuldu.")
            window.location.href = './'
        }

    } catch (error) {
        alert('Bir hata oluştu.')

    }




}