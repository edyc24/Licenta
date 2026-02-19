package com.example.ajfilfov.service;

import com.example.ajfilfov.entity.Invoice;
import com.example.ajfilfov.entity.InvoiceData;
import com.example.ajfilfov.repository.InvoiceRepository;
import com.itextpdf.kernel.pdf.PdfWriter;
import com.itextpdf.kernel.pdf.PdfDocument;
import com.itextpdf.layout.Document;
import com.itextpdf.layout.element.Paragraph;
import org.springframework.stereotype.Service;
import java.io.ByteArrayOutputStream;
import java.util.UUID;

@Service
public class InvoiceService {
    private final InvoiceRepository repo;
    public InvoiceService(InvoiceRepository repo) { this.repo = repo; }

    public byte[] createInvoice(InvoiceData data) {
        try (ByteArrayOutputStream out = new ByteArrayOutputStream()) {
            PdfWriter writer = new PdfWriter(out);
            PdfDocument pdfDoc = new PdfDocument(writer);
            Document document = new Document(pdfDoc);
            document.add(new Paragraph("Factura"));
            document.add(new Paragraph("Număr: " + data.getInvoiceNumber()));
            document.add(new Paragraph("Client: " + data.getRecipientName()));
            document.close();
            byte[] bytes = out.toByteArray();
            Invoice inv = new Invoice();
            inv.setId(UUID.randomUUID());
            inv.setPdfData(bytes);
            repo.save(inv);
            return bytes;
        } catch (Exception e) { throw new RuntimeException("Eroare generare factură", e); }
    }
}
