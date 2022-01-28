/**
 * @type {HTMLInputElement}
 */
const keywordRadio = document.getElementById('keyword-radio');

/**
 * @type {HTMLInputElement}
 */
const fileRadio = document.getElementById('file-radio');

/**
 * @type {HTMLInputElement}
 */
const keyword = document.getElementById('keyword');

/**
 * @type {HTMLInputElement}
 */
const file = document.getElementById('file');

/**
 * @type {HTMLButtonElement}
 */
const submitBtn = document.getElementById('submit-btn');

const keywordInvalidFeedback = document.getElementById('keyword-invalid-feedback');
const fileInvalidFeedback = document.getElementById('file-invalid-feedback');

/**
 * @type {HTMLInputElement}
 */
const googleSuggest = document.getElementById('google-suggest');

/**
 * @type {HTMLInputElement}
 */
const bingSuggest = document.getElementById('bing-suggest');

/**
 * @type {HTMLInputElement}
 */
const duckDuckGoSuggest = document.getElementById('duckduckgo-suggest');

/**
 * @type {HTMLCollectionOf<HTMLInputElement>}
 */
const switchs = document.getElementsByClassName('switch');

disableBtn(submitBtn);
enableInput(keyword);
disableInput(file);

let switchesChecked = false; // TODO: completare

keywordRadio.addEventListener('change', () => {
    if (keywordRadio.checked) {
        clearInput(file);
        enableInput(keyword);
        disableInput(file);
    } else {
        clearInput(keyword);
        disableInput(keyword);
        enableInput(file);
    }
});

fileRadio.addEventListener('change', () => {
    if (fileRadio.checked) {
        clearInput(keyword);
        enableInput(file);
        disableInput(keyword);
    } else {
        clearInput(file);
        disableInput(file);
        enableInput(keyword);
    }
});

keyword.addEventListener('input', () => {
    if (keyword.value.length === 0) {
        setInvalidMessage(keywordInvalidFeedback, 'La keyword non può essere vuota');
        setInvalidInput(keyword);
        disableBtn(submitBtn);
    } else {
        clearInvalidMessage(keywordInvalidFeedback);
        setValidInput(keyword);
        enableBtn(submitBtn);
    }
});

file.addEventListener('input', () => {
    if (file.value.length === 0) {
        setInvalidMessage(fileInvalidFeedback, "E' necessario selezionare un file");
        setInvalidInput(file);
        disableBtn(submitBtn);
    } else {
        clearInvalidMessage(fileInvalidFeedback);
        setValidInput(file);
        enableBtn(submitBtn);
    }
});

for (let i = 0; i < switchs.length; i++) {
    switchs[i].addEventListener('change', () => {
        if (switchs[i].checked && keyword.length > 0) {
            enableBtn(submitBtn);
            switchesChecked = true;
        }
        if (!googleSuggest.checked && !bingSuggest.checked && !duckDuckGoSuggest.checked) {
            disableBtn(submitBtn);
            switchesChecked = false;
        }
    });
}

/**
 * 
 * @param {HTMLButtonElement} button 
 */
function disableBtn(button) {
    button.setAttribute('disabled', 'disabled');
}

/**
 * 
 * @param {HTMLButtonElement} button 
 */
function enableBtn(button) {
    button.removeAttribute('disabled');
}

/**
 * 
 * @param {HTMLElement} element 
 * @param {string} message 
 */
function setInvalidMessage(element, message) {
    element.innerText = message;
}

/**
 * 
 * @param {HTMLElement} element 
 */
function clearInvalidMessage(element) {
    element.innerText = '';
}

/**
 * 
 * @param {HTMLInputElement} input 
 */
function setValidInput(input) {
    input.classList.remove('is-invalid');
}

/**
 * 
 * @param {HTMLInputElement} input 
 */
function setInvalidInput(input) {
    input.classList.add('is-invalid');
}

/**
 * 
 * @param {HTMLInputElement} input 
 */
function clearInput(input) {
    input.value = '';
}

/**
 * 
 * @param {HTMLInputElement} input 
 */
function enableInput(input) {
    input.removeAttribute('disabled');
}

/**
 * 
 * @param {HTMLInputElement} input 
 */
function disableInput(input) {
    input.setAttribute('disabled', 'disabled');
}