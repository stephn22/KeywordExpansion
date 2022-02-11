const submitBtn = document.getElementById('submit-btn');
const selectCulture = document.getElementById('select-culture');
const cultureInvalidFeedback = document.getElementById('culture-invalid-feedback');

disableElement(submitBtn);

selectCulture.addEventListener('change', () => {
    if (selectCulture.value.length > 0) {
        clearInvalidMessage(cultureInvalidFeedback);
        setValidInput(selectCulture);
        enableElement(submitBtn);
    } else {
        setInvalidInput(selectCulture);
        setInvalidMessage(cultureInvalidFeedback, 'La cultura non può essere vuota');
        disableElement(submitBtn);
    }
});

submitBtn.addEventListener('click', () => {
    submitBtn.innerText = '';
    submitBtn.innerHTML = `<div class="spinner-border spinner-border-sm text-light" role="status">
                               <span class="visually-hidden">Loading...</span>
                           </div>`;
});

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