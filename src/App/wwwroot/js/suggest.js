const googleSuggestBtn = document.getElementById('google-suggest-btn');

googleSuggestBtn.addEventListener('click', () => {
    fetch('/api/suggest/ciao/it/it',
        {
            method: 'GET'
        }).then((res) => {
        if (res.status === 200) {
            res.text((data) => {
                console.log(data);
            });
        }
    });
});