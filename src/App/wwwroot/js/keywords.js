const searchKeywords = document.getElementById('search-keyword');
const table = document.getElementById('keywords-table');
const tr = table.getElementsByTagName('tr');

searchKeywords.addEventListener('keyup', () => {
    for (let i = 0; i < tr.length; i++) {
        const td = tr[i].getElementsByTagName('td')[1];

        if (td) {
            const text = td.textContent || td.innerText;

            if (text.indexOf(searchKeywords.value) > -1) {
                fadeIn(tr[i]);
            } else {
                fadeOut(tr[i]);
            }
        }
    }
});

/**
 * Animazione fade in (0.3s)
 * @param {Element} element
 */
function fadeIn(element) {
    element.removeAttribute('hidden');

    setTimeout(() => {
        element.classList.remove('fade-effect');
    }, 280);
}

/**
 * Animazione fade out (0.3s)
 * @param {Element} element
 */
function fadeOut(element) {
    element.classList.add('fade-effect');
    setTimeout(() => {
        element.setAttribute('hidden', '');
    }, 350);
}