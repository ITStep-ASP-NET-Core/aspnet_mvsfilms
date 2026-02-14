

const actorsSelect = document.getElementById("actorsSelect");
const selectedActorsContainer = document.getElementById("selectedActors");
Initialize("actor", "SelectedActorsIds", selectedActorsContainer);
actorsSelect.addEventListener("change", () => manageSelect("actor", actorsSelect, "SelectedActorsIds", selectedActorsContainer));

const authorsSelect = document.getElementById("authorsSelect");
const selectedAuthorsContainer = document.getElementById("selectedAuthors");
Initialize("author", "SelectedAuthorsIds", selectedAuthorsContainer);
authorsSelect.addEventListener("change", () => manageSelect("author", authorsSelect, "SelectedAuthorsIds", selectedAuthorsContainer));

const genreSelect = document.getElementById("genreSelect");
const selectedGenresContainer = document.getElementById("selectedGenres");
Initialize("genre", "SelectedGenresIds", selectedGenresContainer);
genreSelect.addEventListener("change", () => manageSelect("genre", genreSelect, "SelectedGenresIds", selectedGenresContainer));

const selects = [
    {
        Select: actorsSelect,
        Container: selectedActorsContainer,
        Error: document.getElementById("actorError"),
        ErrorText: "The SelectActors field is required."
    },
    {
        Select: authorsSelect,
        Container: selectedAuthorsContainer,
        Error: document.getElementById("authorError"),
        ErrorText: "The SelectAuthors field is required."
    },
    {
        Select: genreSelect,
        Container: selectedGenresContainer,
        Error: document.getElementById("genreError"),
        ErrorText: "The SelectGenre field is required."
    },
];

document.querySelector("form").addEventListener("submit", function (e) {
    if (!validateSelects(selects)) {
        e.preventDefault();
    }
});

function validateSelects(array) {
    let result = true;
    for (const elem of array) {
        result = validateSelect(elem.Select, elem.Container, elem.Error, elem.ErrorText);
    }
    return result;
}

function validateSelect(select, container, errorContainer, errorText) {
    if (container.children.length === 0) {
        errorContainer.textContent = errorText;
        select.addEventListener("change", () => { errorContainer.textContent = "" }, { once: true });
        return false;
    }
    return true;
}

function manageSelect(name, select, savePropertyName, container) {
    const selectedId = select.value;

    if (!selectedId) return;

    const stringId = `${name}-${selectedId}`;

    if (document.getElementById(stringId)) {
        select.value = "";
        return;
    }

    const input = document.createElement("input");
    input.id = stringId;
    input.value = select.options[select.selectedIndex].text;

    const hiddenInput = document.createElement("input");
    hiddenInput.type = "hidden";
    hiddenInput.name = savePropertyName;
    hiddenInput.value = selectedId;

    input.addEventListener("click", () => {
        input.remove();
        hiddenInput.remove();
    });

    container.appendChild(input);
    container.appendChild(hiddenInput);

    select.value = "";
}

function Initialize(name, savePropertyName, container) {
    if (container.children.length !== 0) {
        const children = container.querySelectorAll('[data-id][data-name]');
        for (const child of children) {
            const dataId = child.getAttribute('data-id');
            const dataName = child.getAttribute('data-name');

            const input = document.createElement("input");
            input.id = `${name}-${dataId}`;
            input.value = dataName;

            const hiddenInput = document.createElement("input");
            hiddenInput.type = "hidden";
            hiddenInput.name = savePropertyName;
            hiddenInput.value = dataId;

            input.addEventListener("click", () => {
                input.remove();
                hiddenInput.remove();
            });

            container.appendChild(input);
            container.appendChild(hiddenInput);

        }
    }
}