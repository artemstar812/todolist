const input = document.querySelector(".add-task input")
const button = document.querySelector(".add-task button")
const taskList = document.querySelector(".task-list")
const template = document.querySelector("#task-template");

function showTaskOnList(todoItem) {
    if (todoItem.title === "") return;

    const task = template.content.cloneNode(true)

    const label = task.querySelector(".task-text")
    const deleteBtn = task.querySelector(".delete");
    const editBtn = task.querySelector(".edit")
    const checkbox = task.querySelector(".is-completed");

    const li = task.querySelector("li");

    label.textContent = todoItem.title
    checkbox.checked = todoItem.isCompleted

    deleteBtn.addEventListener("click", () => {
        deleteBttn_callback(todoItem.id, li)
    })

    checkbox.addEventListener('change', () => {
        checkbox_callback(todoItem.id, checkbox)
    })

    editBtn.addEventListener("click", () => {
        editBttn_callback(todoItem, checkbox, label)
    })

    taskList.prepend(task);
}

async function deleteBttn_callback(id, li) {
    try {
        await deleteTodo(id)
        li.remove()
    } catch (err) {
        console.error("Error: ", err)
    }
}

async function checkbox_callback(id, checkbox) {
    try {
        var res = await toggleTodo(id)
    } catch (err) {
        console.error("Error: ", err)

        checkbox.checked = !checkbox.checked
    }
}

async function editBttn_callback(todoItem, checkbox, label) {
    var newTitle = prompt("Edit task")

    if (!newTitle)
        return

    try {
        var res = await updateTodo(todoItem.id, {
            id: todoItem.id,
            title: newTitle,
            isCompleted: checkbox.checked
        })

        label.textContent = newTitle;
    } catch(err) {
        console.error("Error: ", err)
    }
}

async function addTask() {
    const text = input.value.trim()
    if (text === "") return;

    let todo = {
        title: text,
        isCompleted: false
    }

    try {
        var res = await createTodo(todo)

        if (!res) {
            console.error("Failed to create task")
            return
        }

        showTaskOnList(res);
        input.value = "";
    } catch (err) {
        console.error("Error: ", err)
    }
}

button.addEventListener("click", addTask)

async function showTodos() {
    const todos = (await getTodos()).result;

    todos.forEach((todo) => showTaskOnList(todo))
}


showTodos()