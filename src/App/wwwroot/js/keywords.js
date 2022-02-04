const searchKeywords = document.getElementById('search-keyword');
const table = document.getElementById('keywords-table');
const tr = table.getElementsByTagName('tr');
const rankBtn = document.getElementById('rank-btn');

rankBtn.addEventListener('mousedown', () => {
    submitBtn.addEventListener('click', () => {
        submitBtn.innerText = '';
        submitBtn.innerHTML = `<div class="spinner-border spinner-border-sm text-primary" role="status">
                               <span class="visually-hidden">Loading...</span>
                           </div>`;
    });
});