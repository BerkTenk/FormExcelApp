function createExcel(){
    var isim = document.getElementById("isim").value;
    var soyisim = document.getElementById("soyisim").value;
    var adres = document.getElementById("adres").value;
    var mail = document.getElementById("mail").value;


    var table = "<table border='1'><tr><td>İsim</td><td>Soyisim</td><td>Adres</td><td>Mail</td></tr>";
    table += "<tr><td>" + isim + "</td><td>" + soyisim + "</td><td>" + adres + "</td><td>" + mail + "</td></tr>";
    table += "</table>";

    
    var blob = new Blob([table], { type: 'application/vnd.openxmlformats-officedocument.spreadsheetml.sheet' });

    
    var link = document.createElement("a");
    link.href = URL.createObjectURL(blob);
    link.download = "output.xlsx";

    
    link.click();
}
