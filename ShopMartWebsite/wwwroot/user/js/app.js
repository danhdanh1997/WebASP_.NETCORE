//variables

const cartBtn = document.querySelector('.cart-btn');
const cartDOM = document.querySelector('.clear-cart');
const cartOverlay = document.querySelector('.cart-overlay');
const cartItems = document.querySelector('.cart-items'); //số lượng trên giỏ hàng.
const cartTotal = document.querySelector('.cart-total');
const clearCartBtn = document.querySelector('.clear-cart');
const cartContent = document.querySelector('.cart-content');
const productsDOM = document.querySelector('.products-center');

//cart
let cart = [];
//buttons
let buttonsDOM = [];

//getting the products
class Products{
    async getProducts(){
        try{
            //lấy data từ file products json
            let result = await fetch('~/user/products.json');
            let data = await result.json();
            let products = data.items;

            products = products.map(item=>{
                const {title, price} = item.fields;
                const {id} = item.sys;
                const image = item.fields.image.fields.file.url;
                return {title,price,id,image};
            });
            localStorage.setItem('products',JSON.stringify(products));
        }catch(error){
            console.log(error);
        }
    }
}
//  Display products
class UI{
    //show danh sách sản phẩm
    displayProducts(products){
        let result = '';
        products.forEach(product =>{
            result +=`
                <!--single product-->
                    <article class="product">
                        <div class="img-container">
                            <img src=${product.image} alt="product" class="product-img">
                            <button class="bag-btn" data-id=${product.id}>
                                <i class="fas fa-shopping-cart"></i>
                                thêm giỏ hàng
                            </button>
                        </div>
                        <a href="./detail.html"><h3>${product.title}</h3></a>
                        <h6>${product.price} VNĐ</h6>
                    </article>
                <!--end of single product-->
            `;
        });
        productsDOM.innerHTML = result;
    }
    displayCarts(carts){
        
        carts.forEach(cart => {
            this.addCartItems(cart);
        })
    }
    getBagButton(){
        const buttons = [...document.querySelectorAll('.bag-btn')];
        buttonsDOM = buttons;
        //vòng lặp các nút mua.
        buttons.forEach(button => {
            let id = button.dataset.id;
            let inCart = cart.find(item => item.id===id);
    
            if(inCart){
                button.innerText = "Đã thêm";
                button.disabled = true;
            }
            //hàm bắt dự kiện click sản phẩm
            button.addEventListener('click',(event) => {
                event.target.innerText = "Đã thêm";
                event.target.disabled = true;
                //get product from products
                let cartItem = {...Storage.getProduct(id), amount:1};
                
                //add product to the cart
                cart = [...cart,cartItem];
                console.log(cart);

                //save cart in local storage
                Storage.saveCart(cart);

                //set cart values
                this.setCartValues(cart);

                //display cart item
                //this.addCartItems(cartItem);

            });
           
        });
    }
    //sét tổng tiền, và số lượng hàng trong giỏ hàng.
    setCartValues(cart){
        let tempTotal = 0;
        let itemsTotal = 0;
        cart.map(item => {
            tempTotal += item.price * item.amount;
            itemsTotal += item.amount;
        });
        cartItems.innerText = itemsTotal;
    }
    //lưu product vào giỏ hàng
    addCartItems(item){
        const tr = document.createElement('tr');
        tr.classList.add('cart-item');
        tr.innerHTML = 
        `
        <td>
            <img src=${item.image} alt="product">
        </td>
        <td>
            <div>
                <h4>${item.title}</h4>
                <small>color : xám</small><br/>
                <small>size : </small>
            </div>
        </td>
        <td><i class="far fa-check-circle"></i></i></td>
        <td>
            <h5>${item.title}</h5>
        </td>
        <td>
            <i class="fas fa-chevron-left" data-id=${item.id}></i>
            <span class="item-amount">${item.amount}</span>
            <i class="fas fa-chevron-right" data-id=${item.id}></i>
        </td>
        <td>
            <span class="remove-item"><i class="far fa-times-circle"></i></i></span>
        </td>
        `;
        cartContent.appendChild(tr);
        
        
    }
}
//local storage
class Storage{
    //hàm lưu danh sách product vào local storage (name: products)
    static saveProducts(products){
        localStorage.setItem('products',JSON.stringify(products));
    }
    //hàm lấy product theo Id trong JSON.
    static getProduct(id){
        let products = JSON.parse(localStorage.getItem('products'));
        return products.find(product => product.id===id);
    }
    //hàm lưu cart vào local storage (name: cart)
    static saveCart(cart){
        localStorage.setItem('cart',JSON.stringify(cart));
    }
    //hàm lấy ra các mặt hàng trong cart.
    static getCart(){
        return localStorage.getItem('cart')?JSON.parse(localStorage.getItem('cart')):[];
    }
    //hàm lấy ra products từ local storage
    static getProductsInLocalStorage(){
        return localStorage.getItem('products')?JSON.parse(localStorage.getItem('products')):[];
    }
}

////handler event DOMContentLoaded

document.addEventListener('DOMContentLoaded',()=>{
    const ui = new UI();
    const products = new Products();
    products.getProducts();
    ui.displayProducts(Storage.getProductsInLocalStorage());
    ui.getBagButton();
});
