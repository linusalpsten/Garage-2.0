function addOnClick() {
    document.getElementById("RegNrLabel").onclick = function () {
        document.location.href = document.getElementById("RegNr").href;
    }
    document.getElementById("ModelLabel").onclick = function () {
        document.location.href = document.getElementById("Model").href;
    }
}

addOnClick();