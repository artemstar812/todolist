const api = "https://localhost:7150/api/todos";

async function getTodos() {
    const res = await fetch(api);
    return res.json();
}

async function getTodoById(id) {
    const res = await fetch(`${api}/${id}`);
    return res.json();
}

async function createTodo(todo) {
    const res = await fetch(api, {
        method: "POST",
        headers: {
            "Content-type": "application/json"
        },
        body: JSON.stringify({
            title: todo.title,
            isCompleted: todo.isCompleted
        })
    })

    return res.json();
}

async function deleteTodo(id) {
    const res = await fetch(`${api}/${id}`, {
        method: "DELETE"
    })

    if (!res.ok)
        throw new Error("API: Deletion error");
}

async function toggleTodo(id) {
    const res = await fetch(`${api}/${id}/toggle`, {
        method: "PATCH"
    })

    if (!res.ok)
        throw new Error("API: Toggleing error");
}

async function updateTodo(id, todo) {
    const res = await fetch(`${api}/${id}`, {
        method: "PUT",
        headers: {
            "Content-Type": "application/json"
        },
        body: JSON.stringify(todo)
    })

    if (!res.ok)
        throw new Error("API: Todo Updating error");
}