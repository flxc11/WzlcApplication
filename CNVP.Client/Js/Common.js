function $$(id) { return document.getElementById(id) };

///*************************************************
//�������Ƿ���ڹ�����Ϣ
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
//��������ȡ��Ʒ��ϸ��Ϣ
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
//�������رվ�Ʒ��ϸ����
//*************************************************/
function CloseWindows() {
    $('#ProductInfo').empty();
    $('#ProductInfo').css('display', 'none');
}

///*************************************************
//�������رվ�Ʒ��ϸ����
//*************************************************/
function ChangeImagesUrl(Obj) {
    $('#' + Obj).addClass("current").siblings().removeClass("current");
    //alert($('#' + Obj + ' img')[0].src);
}


///*************************************************
//�������ײͼ��빺�ﳵ
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

            //ִ�м������
            $.ajax({ type: "GET", dataType: "JSON", url: "CartList.aspx?Time=" + (new Date().getTime()), data: { Action: "AddGroup", GroupID: GroupID }, success: function (d) { } });
            $(".gwc").addClass("gwc1");
        });
}

///*************************************************
//��������Ʒ���빺�ﳵ
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

            //ִ�м������
            $.ajax({ type: "GET", dataType: "JSON", url: "CartList.aspx?Time=" + (new Date().getTime()), data: { Action: "AddProduct", ProductID: ProductID }, success: function (d) { } });
            $(".gwc").addClass("gwc1");
        });
}

///*************************************************
//��������ʾ�õ�ͼƬ�б�
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
//�������رջõ�ͼƬ�б�
//*************************************************/
function RemoveWindows() {
    $('#Gallery').html('');
    $('#Gallery').remove();
}