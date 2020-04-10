function isLoggedIn() {
    if (!sessionStorage.getItem('accessToken')) {
        alert("not logged in, redirecting to register page")
        window.location.href = "/";
    }
}