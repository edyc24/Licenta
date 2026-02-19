package com.example.ajfilfov.entity;

import jakarta.persistence.*;
import java.util.UUID;

@Entity
@Table(name = "Invoices")
public class Invoice {
    @Id
    @Column(name = "Id")
    private UUID id;
    @Lob
    @Column(name = "PdfData")
    private byte[] pdfData;

    public UUID getId() { return id; }
    public void setId(UUID id) { this.id = id; }
    public byte[] getPdfData() { return pdfData; }
    public void setPdfData(byte[] pdfData) { this.pdfData = pdfData; }
}
