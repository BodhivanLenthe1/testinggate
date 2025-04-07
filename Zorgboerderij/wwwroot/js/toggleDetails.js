function toggleDetails(id, detailClass, detailPrefix) {
    
    const element = document.getElementById(`${detailPrefix}${id}`);
    if (!element) {
        console.warn("⚠️ Geen element gevonden met ID:", `${detailPrefix}${id}`);
        return;
    }

    const isVisible = element.style.display === "block";
    element.style.display = isVisible ? "none" : "block";
    element.style.maxHeight = isVisible ? "0" : "1000px";
    element.style.opacity = isVisible ? "0" : "1";
}