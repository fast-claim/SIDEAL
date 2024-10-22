function modalForms(idForm){
    let titulo=document.getElementById('titleModal'), content=document.getElementById('modalText');
    fetch('js/dailyData/forms.json')
        .then(response => response.json())
        .then(datos => {
            let correctForm = datos.find(item => item.id === idForm);
            if(correctForm){
                document.getElementById('modal').classList.remove('hidden');
                titulo.innerHTML=correctForm.title;
                content.innerHTML=correctForm.modalContent;
            }else{
                console.log("Error");
            }

        })
}

function closeModal() {
    document.getElementById('modal').classList.add('hidden');
}

function toggleSidebar() {
    const sidebar = document.getElementById('sidebar');
    sidebar.classList.toggle('hidden');
}