document.addEventListener('DOMContentLoaded', function () {
    // Toggle dark/light mode
    const themeToggle = document.getElementById('themeToggle');
    const html = document.documentElement;

    // Check for saved theme preference
    const currentTheme = localStorage.getItem('theme') || 'light';
    html.setAttribute('data-bs-theme', currentTheme);
    updateThemeIcon(currentTheme);

    themeToggle.addEventListener('click', function () {
        const newTheme = html.getAttribute('data-bs-theme') === 'dark' ? 'light' : 'dark';
        html.setAttribute('data-bs-theme', newTheme);
        localStorage.setItem('theme', newTheme);
        updateThemeIcon(newTheme);
    });

    function updateThemeIcon(theme) {
        const icon = themeToggle.querySelector('i');
        icon.className = theme === 'dark' ? 'bi bi-sun' : 'bi bi-moon-stars';
    }

    // Form validation
    const form = document.getElementById('calculatorForm');
    if (form) {
        form.addEventListener('submit', function (e) {
            const input = form.querySelector('input[name="Input"]');
            if (!input.value.trim()) {
                e.preventDefault();
                alert('Por favor ingresa números para calcular');
                input.focus();
            }
        });
    }

    // Operation examples
    const operationSelect = document.querySelector('select[name="Operation"]');
    const numberInput = document.querySelector('input[name="Input"]');

    operationSelect.addEventListener('change', function () {
        switch (this.value) {
            case 'add':
                numberInput.placeholder = 'Ej: 5, 10, 15 (suma todos los números)';
                break;
            case 'subtract':
                numberInput.placeholder = 'Ej: 10, 3 (resta el segundo del primero)';
                break;
            case 'multiply':
                numberInput.placeholder = 'Ej: 2, 3, 4 (multiplica todos los números)';
                break;
            case 'divide':
                numberInput.placeholder = 'Ej: 10, 3 (divide el primero por el segundo)';
                break;
            case 'sqrt':
                numberInput.placeholder = 'Ej: 16 (calcula la raíz cuadrada)';
                break;
        }
    });
});