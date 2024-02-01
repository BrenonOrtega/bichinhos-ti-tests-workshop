use actix_web::{get, web, App, HttpServer, Responder};
use serde::Serialize;

#[derive(Serialize)]
struct CalculatorResult {
    net_value: f64,
}

#[get("/api/v1/calculator/{totalIncome}")]
async fn calculate(params: web::Path<f64>) -> impl Responder {
    let total_income = params.into_inner();

    let net_value = if total_income > 4999.0 { total_income - 15.0 } 
        else { 5000.0 };

    let result = CalculatorResult {
        net_value
    };

    web::Json(result)
}

#[actix_web::main]
async fn main() -> std::io::Result<()> {
    HttpServer::new(|| {
        App::new().service(calculate)
    })
    .bind("localhost:2005")?
    .run()
    .await
}