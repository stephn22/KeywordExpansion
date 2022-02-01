const submitBtn = document.getElementById('submit-btn');
const selectCulture = document.getElementById('select-culture');

disableElement(submitBtn);

selectCulture.addEventListener('change', () => {
    if (selectCulture.value.length > 0) {
        disableElement(submitBtn);
    } else {
        enableElement(submitBtn);
    }
});

submitBtn.addEventListener('click', () => {
    /*disableBtn(submitBtn);*/
    submitBtn.innerText = '';
    submitBtn.innerHTML = `<div class="spinner-border spinner-border-sm text-primary" role="status">
                               <span class="visually-hidden">Loading...</span>
                           </div>`;
});

/**
 * 
 * @param {HTMLElement} element 
 */
function disableElement(element) {
    button.setAttribute('disabled', 'disabled');
}

/**
 * 
 * @param {HTMLElement} element 
 */
function enableElement(element) {
    button.removeAttribute('disabled');
}