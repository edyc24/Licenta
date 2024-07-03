$(document).ready(function () {
    let passwordInput = $("#Password");
    let confirmedPasswordInput = $("#ConfirmedPassword");
    let passwordMatchError = $("#passwordMatchError");

    confirmedPasswordInput.on("input", function () {
        let password = passwordInput.val();
        let confirmedPassword = confirmedPasswordInput.val();
        let passwordsMatch = password === confirmedPassword;

        passwordMatchError.toggle(!passwordsMatch);
    });
});