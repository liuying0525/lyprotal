var imgList = [{active: 'imgs/icon-index40.png', normal: 'imgs/icon-index4.png',url:'index'},
{active: 'imgs/icon-index00.png', normal: 'imgs/icon-index0.png',url:'platform'},
{active: 'imgs/icon-index10.png', normal: 'imgs/icon-index1.png',url:'dynamic'},
{active: 'imgs/icon-index20.png', normal: 'imgs/icon-index2.png',url:'management'},
{active: 'imgs/icon-index30.png', normal: 'imgs/icon-index3.png',url:'employees'}];
console.log(window.location.href);
function activeImgFunc (e) {
let eles = document.getElementsByClassName('type');
console.log(eles.length);
for (let i = 0;i < eles.length; i++) {
eles[i].className = 'type';
eles[i].firstChild.src = imgList[i]['normal'];

}
let ele = eles[e];
ele.className = 'activeType';
ele.setAttribute('class', 'type activeType');
ele.firstChild.src = imgList[e]['active'];
window.location.href=imgList[e]['url']+'.html';
}
function activeTypeFunc (e) {
let eles = document.getElementsByClassName('twoType');
for (let i = 0;i < eles.length; i++) {
eles[i].className = 'twoType';
}
let ele = eles[e];
ele.className = 'twoType activeTwoType';
};
$(function(){
    $(".set-middle>div").hide(); // Initially hide all content
	$(".set-title li:first").addClass("current"); // Activate first tab
    $(".set-middle>div:first").fadeIn(); // Show first tab content
    $('.set-title').on("click","li",function(e) {
        e.preventDefault();        
        $(".set-middle>div").hide(); //Hide all content
        $(".set-title li").removeClass("current"); //Reset id's
        $(this).addClass("current"); // Activate this
        $(".set-middle>div").eq($(this).attr("data-li")).fadeIn();   
    });

    $(".description").on("click",".set-middle-content>ul>li",function(e){
        var dynamicindex=$(this).index()
        console.log($(this).index());
        var classNamelistlen=$(this).parents(".set-middle-item")[0].classList.length-1
var nowurl=$(this).parents(".set-middle-item")[0].classList[classNamelistlen];

if($(this).parents(".employees").length==0&&dynamicindex==0){
        return
}
if($(this).parents(".employees").length==0){
    window.location.href="dydescription.html?"+nowurl+"="+dynamicindex;
}else{
    window.location.href=nowurl+"Des.html?"+nowurl+"="+dynamicindex;
}
           
        
    });

    




});


