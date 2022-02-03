const popoverTriggerList = [].slice.call(document.querySelectorAll('[data-bs-toggle="popover"]'));
const popoverList = popoverTriggerList.map(function (popoverTriggerEl) {
    return new bootstrap.Popover(popoverTriggerEl);
});

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
 * @type {HTMLSelectElement}
 */
const selectCulture = document.getElementById('select-culture');

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
        enableInput(selectCulture);
        disableInput(file);
        setRequired(keyword);
        setRequired(selectCulture);
        unsetRequired(file);
    } else {
        clearInput(keyword);
        disableInput(keyword);
        disableInput(selectCulture);
        enableInput(file);
        setRequired(file);
        unsetRequired(keyword);
        unsetRequired(selectCulture);
    }
});

fileRadio.addEventListener('change', () => {
    if (fileRadio.checked) {
        clearInput(keyword);
        enableInput(file);
        disableInput(keyword);
        disableInput(selectCulture);
        setRequired(file);
        unsetRequired(keyword);
        unsetRequired(selectCulture);
    } else {
        clearInput(file);
        disableInput(file);
        enableInput(keyword);
        enableInput(selectCulture);
        setRequired(keyword);
        setRequired(selectCulture);
        unsetRequired(file);
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

submitBtn.addEventListener('mousedown', () => {
    /*disableBtn(submitBtn);*/
    submitBtn.innerText = '';
    submitBtn.innerHTML = `<div class="spinner-border spinner-border-sm text-primary" role="status">
                               <span class="visually-hidden">Loading...</span>
                           </div>`;
});

/**
 *
 * @param {HTMLInputElement} input
 */
function setRequired(input) {
    input.attributes.setNamedItem(document.createAttribute('required'));
}

/**
 *
 * @param {HTMLInputElement} input
 */
function unsetRequired(input) {
    input.attributes.removeNamedItem('required');
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