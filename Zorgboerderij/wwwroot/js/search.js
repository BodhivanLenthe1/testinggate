window.searchList = function (inputId, itemSelector) {
    const input = document.getElementById(inputId);
    const filter = input.value.toLowerCase();
    const items = document.querySelectorAll(itemSelector);

    items.forEach(item => {
        const text = item.textContent.toLowerCase();
        const match = text.includes(filter);
        item.closest("li").style.display = match ? "" : "none";
    });
}
