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
const cultureInvalidFeedback = document.getElementById('culture-invalid-feedback');
const serviceInvalidFeedback = document.getElementById('service-invalid-feedback');

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

disableElement(submitBtn);
enableInput(keyword);
disableInput(file);

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
    if (keyword.value.length > 0) {
        clearInvalidMessage(keywordInvalidFeedback);
        setValidInput(keyword);

        if (switchesChecked() && selectCulture.value.length > 0) {
            enableElement(submitBtn);
        } else {
            disableElement(submitBtn);
        }
    } else {
        setInvalidMessage(keywordInvalidFeedback, 'La keyword non può essere vuota');
        setInvalidInput(keyword);
        disableElement(submitBtn);
    }
});

selectCulture.addEventListener('change', () => {
    if (selectCulture.value.length > 0) {
        setValidInput(selectCulture);
        clearInvalidMessage(cultureInvalidFeedback);

        if (switchesChecked() && keyword.value.length > 0) {
            enableElement(submitBtn);
        } else {
            disableElement(submitBtn);
        }

    } else {
        setInvalidInput(selectCulture);
        disableElement(submitBtn);
        setInvalidMessage(cultureInvalidFeedback, 'La cultura non può essere vuota');
    }
});

file.addEventListener('input', () => {
    if (file.value.length > 0) {
        clearInvalidMessage(fileInvalidFeedback);
        setValidInput(file);

        if (switchesChecked()) {
            enableElement(submitBtn);
        } else {
            disableElement(submitBtn);
        }
    } else {
        setInvalidMessage(fileInvalidFeedback, "E' necessario selezionare un file");
        setInvalidInput(file);
        disableElement(submitBtn);
    }
});

for (let i = 0; i < switchs.length; i++) {
    switchs[i].addEventListener('change', () => {
        if ((switchs[i].checked && keyword.value.length > 0 && selectCulture.value.length > 0)
             || switchs[i].checked && file.value.length > 0) {
             enableElement(submitBtn);
         } else {
             disableElement(submitBtn);
         }
    });
}

submitBtn.addEventListener('click', () => {
    submitBtn.innerText = '';
    submitBtn.innerHTML = `<div class="spinner-border spinner-border-sm text-light" role="status">
    <span class="visually-hidden">Loading...</span>
    </div>`;
});

function switchesChecked() {
    for (let i = 0; i < switchs.length; i++) {
        if (switchs[i].checked) {
            return true;
        }
    }
    return false;
}

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
* @param {HTMLElement} element 
*/
function disableElement(element) {
    element.setAttribute('disabled', 'disabled');
}

/**
* 
* @param {HTMLElement} element 
*/
function enableElement(element) {
    element.removeAttribute('disabled');
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