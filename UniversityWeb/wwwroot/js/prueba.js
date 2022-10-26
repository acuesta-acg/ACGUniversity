"use strict";

var con = new signalR.HubConnectionBuilder().withUrl("/HubPrueba")
    .withHubProtocol(new signalR.protocols.msgpack.MessagePackHubProtocol())
    .build();

document.getElementById("enviar").disabled = true;

con.on("MensajeRecibido", function (usr, mensaje) {
    var li = document.createElement("li");
    document.getElementById("listaMensajes").appendChild(li);
    li.textContent = `${usr} dice ${mensaje}`;
});

con.on("MensajeEnviado", function (mensaje) {
    var li = document.createElement("li");
    document.getElementById("listaMensajes").appendChild(li);
    li.textContent = `Mensaje enviado por ${mensaje.Usuario}:  ${mensaje.Texto} (valor ${mensaje.Valor})`;
});

con.on("IdRecibido", function (id) {
    var li = document.createElement("li");
    document.getElementById("listaMensajes").appendChild(li);
    li.textContent = `Id de conexión: ${id}`;
});

con.start().then(function () {
    document.getElementById("enviar").disabled = false;
}).catch(function (err) { return console.error(err.toString()) });

document.getElementById("enviar").addEventListener("click", function (event) {
    var usr = document.getElementById("usuarioInput").value;
    var mensaje = document.getElementById("mensajeInput").value;
    /*
    con.invoke("EnviarMsg", usr, mensaje).catch(function (err) {
        return console.error(err.toString());
    }); */

    con.invoke("EnviarMsg2", mensaje).catch(function (err) {
        return console.error(err.toString());
    });

    event.preventDefault();
});