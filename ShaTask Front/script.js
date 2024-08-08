const apiUrl = 'https://localhost:44358/api/';

// Fetch data from the API
fetch(apiUrl+"InvoiceData")
    .then(response => response.json())
    .then(data => {
        const dataDiv = document.getElementById('data');
        for(let i=0;i<data.length;i++){
            const listItems = data[i].invoiceItems.map(item => `<li class="list-group-item">${item.itemName} (Qty: ${item.itemCount}) Price: ${item.itemPrice} Total item Price: ${item.totalPriceItem}</li>`).join('');
            dataDiv.innerHTML += `
          <div class="card col-5 m-2">
            <div class="card-body">
              <h5 class="card-title">ID: ${data[i].invoiceHeaderId}</h5>
              <h6 class="card-subtitle mb-2"><span style="color:red;">Customer:</span> ${data[i].customerName} <span style="color:red;">Date:</span> ${data[i].invoiceDate} </h6>
              <h6 class="card-subtitle mb-2"><span style="color:red;">Cashier ID:</span> ${data[i].cashierId}  <span style="color:red;">Cashier Name:</span> ${data[i].cashierName} </h6>

              <p class="card-text">Branch ID: ${data[i].branchId} -- Branch Name: ${data[i].branchName}</p>
                <p class="card-text" style="color:red;">Items:</p>
               <ul class="list-group list-group-flush">
                    ${listItems} <!-- Dynamically generated list items -->
                </ul>
            </div>
            <button type="button" class="btn btn-danger" onclick="deleteInvoice(${data[i].invoiceHeaderId})">Delete</button>
            
          </div>
            
            `
        }

              
    })
    .catch(error => console.error('Error fetching data:', error));


    fetch(apiUrl+"Cashier")
    .then(response => response.json())
    .then(data => {
        const dataDiv = document.getElementById('cashier');
        for(let i=0;i<data.length;i++){
            dataDiv.innerHTML += `
          <div class="card col-5 m-2">
            <div class="card-body">
              <h5 class="card-title">ID: ${data[i].id}</h5>
              <h6 class="card-subtitle mb-2">Name: ${data[i].name}</h6>
              <h6 class="card-subtitle mb-2">Branch ID: ${data[i].branchId}  Branch Name: ${data[i].branchName}</h6>
            </div>
            <button type="button" class="btn btn-danger" onclick="deleteCashier(${data[i].id})">Delete</button>
            
          </div>
            
            `
        }

              
    })
    .catch(error => console.error('Error fetching data:', error));

    async function deleteInvoice(id) {
        try {
            const response = await fetch(`${apiUrl}InvoiceData/${id}`, {
                method: 'DELETE',
                headers: {
                    'Content-Type': 'application/json'
                }
            });
    
            // Remove the button's parent element if it exists
            const button = document.querySelector(`button[onclick="deleteInvoice(${id})"]`);
            if (button) {
                button.parentElement.remove(); // Remove the parent element (div) of the button
            }
        } catch (error) {
            console.error('Error deleting data:', error);
        }
    }

    async function deleteCashier(id) {
        try {
            const response = await fetch(`${apiUrl}Cashier/${id}`, {
                method: 'DELETE',
                headers: {
                    'Content-Type': 'application/json'
                }
            });
    
            // Remove the button's parent element if it exists
            const button = document.querySelector(`button[onclick="deleteCashier(${id})"]`);
            if (button) {
                button.parentElement.remove(); // Remove the parent element (div) of the button
            }
        } catch (error) {
            console.error('Error deleting data:', error);
        }
    }
    

