(() => {
    const overlay = document.getElementById("flashlight");
    const body = document.querySelector(".home-night");
    if (!overlay || !body) {
        return;
    }

    let active = false;
    const letters = document.querySelectorAll(".letter-field span");
    const FLASHLIGHT_RADIUS = 70;

    const setPosition = (x, y) => {
        overlay.style.setProperty("--flash-x", `${x}px`);
        overlay.style.setProperty("--flash-y", `${y}px`);
    };

    const setFlashlightState = (state) => {
        active = state;
        overlay.classList.toggle("active", state);
        body.classList.toggle("flashlight-active", state);
    };

    const clearLetters = () => {
        letters.forEach((letter) => letter.classList.remove("lit"));
    };

    const updateLetters = (x, y) => {
        letters.forEach((letter) => {
            const rect = letter.getBoundingClientRect();
            const centerX = rect.left + rect.width / 2;
            const centerY = rect.top + rect.height / 2;
            const distance = Math.hypot(centerX - x, centerY - y);
            letter.classList.toggle("lit", distance < FLASHLIGHT_RADIUS);
        });
    };

    const handlePointerDown = (event) => {
        if (event.button !== 0) {
            return;
        }

        setFlashlightState(true);
        setPosition(event.clientX, event.clientY);
        updateLetters(event.clientX, event.clientY);
    };

    const handlePointerMove = (event) => {
        if (!active) {
            return;
        }

        setPosition(event.clientX, event.clientY);
        updateLetters(event.clientX, event.clientY);
    };

    const handlePointerUp = () => {
        setFlashlightState(false);
        clearLetters();
    };

    body.addEventListener("pointerdown", handlePointerDown);
    window.addEventListener("pointermove", handlePointerMove);
    window.addEventListener("pointerup", handlePointerUp);

    const secretTrigger = document.querySelector(".secret-body.secret-trigger");
    const secretPanel = document.getElementById("secretInputPanel");
    const secretInput = document.getElementById("secretInput");
    const secretSubmit = document.getElementById("secretSubmit");
    const secretFeedback = document.getElementById("secretFeedback");
    const expectedSecret = (secretTrigger?.dataset?.secret ?? "").toString().toUpperCase();

    const showSecretPanel = () => {
        if (!secretPanel) {
            return;
        }
        secretPanel.classList.add("visible");
        secretPanel.setAttribute("aria-hidden", "false");
        secretInput?.focus();
    };

    window.showSecretPanel = showSecretPanel;

    const hideSecretPanel = () => {
        if (!secretPanel) {
            return;
        }
        secretPanel.classList.remove("visible");
        secretPanel.setAttribute("aria-hidden", "true");
        if (secretInput) {
            secretInput.value = "";
            secretInput.classList.remove("invalid");
        }
        secretPanel.classList.remove("invalid");
        if (secretFeedback) {
            secretFeedback.textContent = "Only the correct combination will open the door.";
        }
    };

    const checkSecret = async () => {
        const code = secretInput.value.trim().toUpperCase();
        
        try {
            // First, store the code via POST with header
            
            const response = await fetch(`/Rooms/Index?code=${encodeURIComponent(code)}`, {
                method: "POST"
            });

            debugger;
            if (response.ok)
            {
                window.location.href = `/Rooms/Room1`;
            } else {
                secretPanel.classList.add("invalid");
                secretFeedback.textContent = "Nothing happened. Try again.";
                setTimeout(() => secretPanel.classList.remove("invalid"), 1200);
            }
        } catch (error) {
            console.error("Error navigating to Step1:", error);
            secretPanel.classList.add("invalid");
            secretFeedback.textContent = "An error occurred. Try again.";
            setTimeout(() => secretPanel.classList.remove("invalid"), 1200);
        }
    };

    secretSubmit?.addEventListener("click", () => checkSecret());
    secretInput?.addEventListener("keydown", (event) => {
        if (event.key === "Enter") {
            event.preventDefault();
            checkSecret();
        }
    });

    secretTrigger?.addEventListener("click", (event) => {
        event.preventDefault();
        showSecretPanel();
    });

    document.addEventListener("click", (event) => {
        if (
            secretPanel?.classList.contains("visible") &&
            secretPanel !== event.target &&
            !secretPanel.contains(event.target) &&
            secretTrigger &&
            secretTrigger !== event.target &&
            !secretTrigger.contains(event.target)
        ) {
            hideSecretPanel();
        }
    });

    // Function to call Step1Controller with a string parameter
    const callStep1 = async (data) => {
        try {
            const response = await fetch("/Step1/ReceiveData", {
                method: "POST",
                headers: {
                    "Content-Type": "application/json"
                },
                body: JSON.stringify(data)
            });

            if (response.ok) {
                const result = await response.json();
                console.log("Step1 response:", result);
                return result;
            } else {
                const errorText = await response.text();
                console.error("Error calling Step1:", response.statusText, errorText);
                return null;
            }
        } catch (error) {
            console.error("Error calling Step1:", error);
            return null;
        }
    };
})();

