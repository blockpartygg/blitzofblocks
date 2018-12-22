(function() {
    const gameDiv = document.querySelector("#game");
    const initialGameDivDimensions = {
        width: parseInt(gameDiv.style.width, 10),
        height: parseInt(gameDiv.style.height, 10)
    };
    gameDiv.style.width = "100%";
    gameDiv.style.height = "100%";
    const setDimensions = () => {
        gameDiv.style.position = "absolute";
        const canvas = document.getElementsByTagName("canvas")[0];
        const computedGameDivWidth = parseInt(window.getComputedStyle(gameDiv).width, 10);
        const computedGameDivHeight = parseInt(window.getComputedStyle(gameDiv).height, 10);
        const scale = Math.min(computedGameDivWidth / initialGameDivDimensions.width, computedGameDivHeight / initialGameDivDimensions.height);
        const scaledWidth = Math.round(initialGameDivDimensions.width * scale * 100) / 100;
        const scaledHeight = Math.round(initialGameDivDimensions.height * scale * 100) / 100;
        if(canvas) {
        canvas.style.width = scaledWidth + "px";
        canvas.style.height = scaledHeight + "px";
        }
    }
    window.setDimensions = setDimensions;
    let resizeTimer;
    window.addEventListener('resize', () => {
        clearTimeout(resizeTimer);
        resizeTimer = setTimeout(setDimensions, 200);
    });
    clearTimeout(resizeTimer);
    resizeTimer = setTimeout(setDimensions, 3000);
})();