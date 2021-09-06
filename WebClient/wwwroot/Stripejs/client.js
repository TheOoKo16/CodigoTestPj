// A reference to Stripe.js initialized with your real test publishable API key.
var stripe = Stripe("pk_test_51JWbVTKgo0AFtVuQQdMsrEDDd8Bhbh8utUeT0vob9snh9jZaOgZtGTFjyTPJE9zuXkA7mWxmQeb4KQlcNJ4vtFSF00ohgHH9cp");




//document.querySelector("button").disabled = true;
var style = {
    base: {
        color: "#32325d",
        fontFamily: 'Arial, sans-serif',
        fontSmoothing: "antialiased",
        fontSize: "16px",
        "::placeholder": {
            color: "#32325d"
        }
    },
    invalid: {
        fontFamily: 'Arial, sans-serif',
        color: "#fa755a",
        iconColor: "#fa755a"
    }
};
var elements = stripe.elements();
card = elements.create("card", { style: style });
// Stripe injects an iframe into the DOM
card.mount("#card-element");

card.on("change", function (event) {
    // Disable the Pay button if there are no card details in the Element
    //  document.querySelector("button").disabled = event.empty;
    //  document.querySelector("#card-error").textContent = event.error ? event.error.message : "";
});
// Calls stripe.confirmCardPayment
// If the card requires authentication Stripe shows a pop-up modal to
// prompt the user to enter authentication details without leaving your page.
var payWithCard = function (stripe, card, clientSecret) {
    //  loading(true);
    stripe
        .confirmCardPayment(clientSecret, {
            payment_method: {
                card: card
            }
        })
        .then(function (result) {
            if (result.error) {
                // Show error to your customer
                showError(result.error.message);
            } else {
                alert("Please check for payment success " + "https://dashboard.stripe.com/test/payments/" + result.paymentIntent.id);
                // The payment succeeded!
                orderComplete(result.paymentIntent.id);
            }
        });
};

/* ------- UI helpers ------- */

// Shows a success message when the payment is complete
var orderComplete = function (paymentIntentId) {
    //  loading(false);
    //document
    //    .querySelector(".result-message a")
    //    .setAttribute(
    //        "href",
    //        "https://dashboard.stripe.com/test/payments/" + paymentIntentId
    //);

    location.href = "../Purchase/Index";

};

// Show the customer the error from Stripe if their card fails to charge
var showError = function (errorMsgText) {
    //  loading(false);
    alert(errorMsgText);
    //var errorMsg = document.querySelector("#card-error");
    //errorMsg.textContent = errorMsgText;
    //setTimeout(function () {
    //    errorMsg.textContent = "";
    //}, 4000);
};