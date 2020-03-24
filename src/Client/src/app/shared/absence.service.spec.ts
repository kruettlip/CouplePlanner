import { AbsenceService } from './absence.service';

describe('AbsenceService', () => {
  let service: AbsenceService;

  beforeEach(() => {
    service = new AbsenceService(null);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
