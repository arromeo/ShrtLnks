// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
const links = document.getElementsByClassName("copy-link");

if (links.length > 0) {
    for (let i = 0; i < links.length; i++) {
        links[i].addEventListener("click", function (event) {
            const shortUrl = event.target.id;
            const el = document.createElement('textarea');
            el.value = 'https://localhost:44349/' + shortUrl;
            document.body.appendChild(el);
            el.select();
            document.execCommand("copy");
            document.body.removeChild(el);
        });
    }
}