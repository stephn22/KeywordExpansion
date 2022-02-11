const searchKeywords = document.getElementById('search-keyword');
const table = document.getElementById('keywords-table');
const tr = table.getElementsByTagName('tr');
const rankBtn = document.getElementById('rank-btn');
const exportBtn = document.getElementById('export-keywords');

rankBtn.addEventListener('click', () => {
    rankBtn.innerText = '';
    rankBtn.innerHTML = `<div class="spinner-border spinner-border-sm text-light" role="status">
                               <span class="visually-hidden">Loading...</span>
                           </div>`;
});