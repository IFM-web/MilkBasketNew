$(document).ready(() => {
    localStorage.clear();
    console.log(window.location.hostname);
    if (window.location.hostname == "localhost") {
        localStorage.setItem("Url", '');
    } else {
        localStorage.setItem("Url", '/MilkBasket1');
        
       
    }
})



