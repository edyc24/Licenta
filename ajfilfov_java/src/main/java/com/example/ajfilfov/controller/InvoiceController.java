package com.example.ajfilfov.controller;

import com.example.ajfilfov.entity.InvoiceData;
import com.example.ajfilfov.service.InvoiceService;
import org.springframework.http.*;
import org.springframework.web.bind.annotation.*;

@RestController
@RequestMapping("/api/invoices")
public class InvoiceController {
    private final InvoiceService service;
    public InvoiceController(InvoiceService service) { this.service = service; }

    @PostMapping("/create")
    public ResponseEntity<byte[]> createInvoice(@RequestBody InvoiceData data) {
        byte[] pdf = service.createInvoice(data);
        return ResponseEntity.ok()
            .header(HttpHeaders.CONTENT_DISPOSITION, "attachment; filename=invoice.pdf")
            .contentType(MediaType.APPLICATION_PDF)
            .body(pdf);
    }
}
