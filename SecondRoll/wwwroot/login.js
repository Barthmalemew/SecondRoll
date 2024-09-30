document.getElementById('login-form').addEventListener('submit', async function (event) {
    event.preventDefault();

    const username = document.getElementById('username').value;
    const password = document.getElementById('password').value;

    const response = await fetch('/api/player/login', {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json'
        },
        body: JSON.stringify({
            username: username,
            password: password
        })
    });

    const messageElement = document.getElementById('login-message');
    if (response.ok) {
        const data = await response.json();
        messageElement.innerText = data.message;
        localStorage.setItem('username', username); // Save username
        window.location.href = '/index.html'; // Redirect to chat page
    } else {
        const error = await response.json();
        messageElement.innerText = error.message;
    }
});
