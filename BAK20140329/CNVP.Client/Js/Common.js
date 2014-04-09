function $$(id) { return document.getElementById(id) };

///*************************************************
//描述：是否存在购物信息
//*************************************************/
function GetCartInfo() {
    $.ajax({ type: "GET", dataType: "JSON", url: "CartList.aspx?Time=" + (new Date().getTime()), data: { Action: "IsEmpty" },
        success: function (d) {
            if (d.msgCode == "True") {
                $(".gwc").addClass("gwc1");
            }
        }
    });
}

///*************************************************
//描述：获取精品详细信息
//*************************************************/
function GetProductInfo(TypeID) {
    $('#ProductInfo').css('display', '');
    $("#Loading").show();

    $.ajax({ async: "true", type: "GET", dataType: "HTML", url: "ProductInfo.aspx?Time=" + (new Date().getTime()), data: { TypeID: TypeID }, cache: false,
        success: function (d) {
            var height = $(window).height();
            $('#Part').css('height', (height - 100) + 'px');
            $(d).appendTo($('#ProductInfo'));
        }
    });
    $("#Loading").hide();
}

///*************************************************
//描述：关闭精品详细窗口
//*************************************************/
function CloseWindows() {
    $('#ProductInfo').empty();
    $('#ProductInfo').css('display', 'none');
}

///*************************************************
//描述：关闭精品详细窗口
//*************************************************/
function ChangeImagesUrl(Obj) {
    $('#' + Obj).addClass("current").siblings().removeClass("current");
    //alert($('#' + Obj + ' img')[0].src);
}


///*************************************************
//描述：套餐加入购物车
//*************************************************/
function GroupAddCart(GroupID) {
    var ProductX = $('#FlyToCart').offset().left;
    var ProductY = $('#FlyToCart').offset().top;

    var CartX = $('.gwc').offset().left;
    var CartY = $('.gwc').offset().top;

    var ImageWidth = $('#FlyToCart').width() / 5;
    var ImageHeight = $('#FlyToCart').height() / 5;

    var CartInfo = document.createElement("DIV");
    if (CartInfo) {
        CartInfo.name = "CartInfo";
        CartInfo.id = "CartInfo";
        document.body.appendChild(CartInfo);
    }

    $('#CartInfo').html('');
    $('#CartInfo').css({ 'position': 'absolute', 'top': ProductY, 'left': ProductX, 'z-index': '999' });

    $('#FlyToCart').clone()
        .prependTo('#CartInfo')
        .css({ 'position': 'absolute', 'border': '2px solid #ff2400', 'z-index': '999' })
        .animate({ opacity: 1.0 })
        .animate({
            left: CartX - 236 + 'px',
            top: CartY - 135 + 'px',
            opacity: 0.6,
            height: '60px',
            width: '60px',
            'z-index': '999'
        })
        .animate({
            left: CartX - 296 + 'px',
            top: CartY - 135 + 'px',
            opacity: 0.0,
            height: '60px',
            width: '60px',
            'z-index': '999'
        }, 800, function () {
            $('#CartInfo').html('');

            //执行加入操作
            $.ajax({ type: "GET", dataType: "JSON", url: "CartList.aspx?Time=" + (new Date().getTime()), data: { Action: "AddGroup", GroupID: GroupID }, success: function (d) { } });
            $(".gwc").addClass("gwc1");
        });
}

///*************************************************
//描述：精品加入购物车
//*************************************************/
function AddCart(ProductID) {
    var ProductX = $('#FlyToCart').offset().left;
    var ProductY = $('#FlyToCart').offset().top;

    var CartX = $('.gwc').offset().left;
    var CartY = $('.gwc').offset().top;

    var ImageWidth = $('#FlyToCart').width() / 5;
    var ImageHeight = $('#FlyToCart').height() / 5;

    var CartInfo = document.createElement("DIV");
    if (CartInfo) {
        CartInfo.name = "CartInfo";
        CartInfo.id = "CartInfo";
        document.body.appendChild(CartInfo);
    }

    $('#CartInfo').html('');
    $('#CartInfo').css({ 'position': 'absolute', 'top': ProductY, 'left': ProductX, 'z-index': '999' });

    $('#FlyToCart').clone()
        .prependTo('#CartInfo')
        .css({ 'position': 'absolute', 'border': '2px solid #ff2400', 'z-index': '999' })
        .animate({ opacity: 1.0 })
        .animate({
        left: CartX-12+'px',
        top: CartY-140+'px',
        opacity: 0.6,
        height: '60px',
        width: '60px',
        'z-index': '999'
        })
        .animate({
            left: CartX - 72 + 'px',
            top: CartY - 140 + 'px',
            opacity: 0.0,
            height: '60px',
            width: '60px',
            'z-index': '999'
        }, 800, function () {
            $('#CartInfo').html('');

            //执行加入操作
            $.ajax({ type: "GET", dataType: "JSON", url: "CartList.aspx?Time=" + (new Date().getTime()), data: { Action: "AddProduct", ProductID: ProductID }, success: function (d) { } });
            $(".gwc").addClass("gwc1");
        });
}

///*************************************************
//描述：显示幻灯图片列表
//*************************************************/
function ProductImages(ID, Type) {
    $("#Loading").show();

    var Gallery = document.createElement("DIV");
    if (Gallery) {
        Gallery.name = "Gallery";
        Gallery.id = "Gallery";
        document.body.appendChild(Gallery);
    }
    $('#Gallery').css({ 'position': 'absolute', 'top': '0px', 'left': '0px', width: '100%', height: '100%', 'z-index': '999', background: '#CCC' });
    
    $.ajax({ async: "true", type: "GET", dataType: "HTML", url: "ProductImages.aspx?Time=" + (new Date().getTime()), data: { ID: ID, Type: Type }, cache: false,
        success: function (d) {
            $(d).appendTo($('#Gallery'));
        }
    });
    $("#Loading").hide();
}

///*************************************************
//描述：关闭幻灯图片列表
//*************************************************/
function RemoveWindows() {
    $('#Gallery').html('');
    $('#Gallery').remove();
}