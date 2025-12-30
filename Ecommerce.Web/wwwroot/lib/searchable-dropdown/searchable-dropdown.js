// searchable-dropdown.js
class SearchableDropdown {
    constructor(config) {
        this.container = document.getElementById(config.containerId);
        this.input = document.getElementById(config.inputId);
        this.searchInput = document.getElementById(config.searchInputId);
        this.list = document.getElementById(config.listId);
        this.menu = this.container.querySelector('.dropdown-menu-custom');
        this.data = config.data;
        this.selectedValue = null;
        this.onChange = config.onChange || null; // Optional callback

        this.init();
    }

    init() {
        this.renderItems(this.data);
        this.attachEvents();
    }

    renderItems(items) {
        if (items.length === 0) {
            this.list.innerHTML = '<div class="no-results">No results found</div>';
            return;
        }

        this.list.innerHTML = items.map(item =>
            `<li class="dropdown-item-custom" data-id="${item.id}" data-name="${item.name}">
                ${item.name}
            </li>`
        ).join('');

        this.list.querySelectorAll('.dropdown-item-custom').forEach(item => {
            item.addEventListener('click', () => this.selectItem(item));
        });
    }

    selectItem(item) {
        const id = item.getAttribute('data-id');
        const name = item.getAttribute('data-name');

        this.selectedValue = { id, name };
        this.input.value = name;
        this.closeDropdown();

        this.list.querySelectorAll('.dropdown-item-custom').forEach(i =>
            i.classList.remove('selected')
        );
        item.classList.add('selected');

        // Trigger onChange callback if provided
        if (this.onChange) {
            this.onChange(this.selectedValue);
        }
    }

    filterItems(searchTerm) {
        const filtered = this.data.filter(item =>
            item.name.toLowerCase().includes(searchTerm.toLowerCase())
        );
        this.renderItems(filtered);
    }

    openDropdown() {
        this.menu.classList.add('show');
        this.container.classList.add('active');
        this.searchInput.focus();
    }

    closeDropdown() {
        this.menu.classList.remove('show');
        this.container.classList.remove('active');
        this.searchInput.value = '';
        this.renderItems(this.data);
    }

    attachEvents() {
        this.input.addEventListener('click', () => {
            if (this.menu.classList.contains('show')) {
                this.closeDropdown();
            } else {
                this.openDropdown();
            }
        });

        this.searchInput.addEventListener('input', (e) => {
            this.filterItems(e.target.value);
        });

        this.searchInput.addEventListener('click', (e) => {
            e.stopPropagation();
        });

        document.addEventListener('click', (e) => {
            if (!this.container.contains(e.target)) {
                this.closeDropdown();
            }
        });
    }

    getValue() {
        return this.selectedValue;
    }

    setValue(id) {
        const item = this.data.find(d => d.id == id);
        if (item) {
            this.selectedValue = item;
            this.input.value = item.name;
        }
    }

    reset() {
        this.selectedValue = null;
        this.input.value = '';
        this.list.querySelectorAll('.dropdown-item-custom').forEach(i =>
            i.classList.remove('selected')
        );
    }

    updateData(newData) {
        this.data = newData;
        this.renderItems(this.data);
    }
}