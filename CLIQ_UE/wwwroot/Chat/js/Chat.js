// start Sidebar 

document.querySelector('.Chat_sidebar_profile_toggle').addEventListener('click' , function(e) 
{
    e.preventDefault();
    this.parentElement.classList.toggle('active');
})
document.addEventListener('click' , function (e)
{
    if (!e.target.matches('.Chat_sidebar_profile , .Chat_sidebar_profile *'))
    {
        document.querySelector('.Chat_sidebar_profile').classList.remove('active')
    }
})
// End Sidebar 


// start Conversation 
document.querySelectorAll('.Conversation_Item_dropdown_toggle').forEach (function (item) {
    item.addEventListener('click' , function(e)  {
        e.preventDefault() ; 
        if (this.parentElement.classList.contains('active'))
        {
            this.parentElement.classList.remove('active')
        }
        else 
        {
            document.querySelectorAll('.Conversation_Item_dropdown').forEach( function(i) {
                i.classList.remove('active')
            })
            this.parentElement.classList.add('active')    
        }
        
    })
})
document.addEventListener('click' , function(e) {
    if(!e.target.matches('.Conversation_Item_dropdown ,.Conversation_Item_dropdown *'))
    {
        document.querySelectorAll('.Conversation_Item_dropdown').forEach( function(i) {
            i.classList.remove('active')
        })
    }
} )

document.querySelectorAll('.Conversation_form_input').forEach(function(item) {
    item.addEventListener('input' , function() {
        this.rows =this.value.split('\n').length
    })
})




document.querySelectorAll('[data-conversation]').forEach(function(item) {
    item.addEventListener('click', function(e) {
        e.preventDefault()
        document.querySelectorAll('.Conversation').forEach(function(i) {
            i.classList.remove('active')
        })
        document.querySelector(this.dataset.conversation).classList.add('active')
    })
})
document.querySelectorAll('.Conversation_Back').forEach(function(item) {
    item.addEventListener('click', function(e) {
        e.preventDefault()
        this.closest('.Conversation').classList.remove('active')
        document.querySelector('.Conversation_default').classList.add('active')
    })
})
// End Conversation 

const button = document.querySelector('#emoji-button');

const picker = new EmojiButton();

button.addEventListener('click', () => {
  picker.togglePicker(button);
  
});

  picker.on('emoji', emoji => {
    document.querySelector('textarea').value += emoji;
  });